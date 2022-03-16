using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Master.Models;
using NCrontab.Advanced;
using NCrontab.Advanced.Enumerations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading;
using System.Timers;
using NCrontab.Advanced.Exceptions;

namespace Master.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private readonly TasksContext _context;
        CancellationTokenSource _tokenSource = null;

        public JobsController(TasksContext context)
        {
            _context = context;
        }

        
        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var tasksContext = _context.Jobs.Include(j => j.Group);
            return View(await tasksContext.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .Include(j => j.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            List<SelectListItem> gruppovuoto = new List<SelectListItem>();
            gruppovuoto.Add(new SelectListItem() { Text = "", Value = "" });
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name").Concat(gruppovuoto);//come si mette nella dropdown list il valore "vuoto"
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Command,Arguments,CronString,GroupId,User")] Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            SelectList items = new SelectList(_context.Groups, "Id", "Name", jobs.GroupId);
            List<SelectListItem> gruppovuoto = new List<SelectListItem>();
            gruppovuoto.Add(new SelectListItem { Text = "", Value = "" });

            ViewData["GroupId"] = items.Concat(gruppovuoto);
            return View(jobs);
        }

        [Authorize(Roles = "Admin")]
        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs.FindAsync(id);
            if (jobs == null)
            {
                return NotFound();
            }

            SelectList items = new SelectList(_context.Groups, "Id", "Name", jobs.GroupId);
            List<SelectListItem> gruppovuoto = new List<SelectListItem>();
            gruppovuoto.Add(new SelectListItem { Text = "", Value = "" });

            ViewData["GroupId"] = items.Concat(gruppovuoto);
            return View(jobs);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Command,Arguments,CronString,GroupId,User")] Jobs jobs)
        {
            if (id != jobs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobsExists(jobs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", jobs.GroupId);
            return View(jobs);
        }

        [Authorize(Roles = "Admin")]
        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobs = await _context.Jobs
                .Include(j => j.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobs == null)
            {
                return NotFound();
            }

            return View(jobs);//innescare il processo di rendering
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var jobs = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(jobs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Process(string host, int port, string fileName, string arguments, CancellationToken token)//suffisso -Async non si usa se l'action è implementata come metodo asincrono
        {

            using var httpClient = new HttpClient();
            var json = "";

            await Task.Run(async () => {

                if (token.IsCancellationRequested == false)
                    json = await httpClient.GetStringAsync(new Uri(string.Concat($"http://{host}:{port}/api/process?fileName={fileName}&arguments={arguments}")));
            })
                .ConfigureAwait(false);


            var result = JsonSerializer.Deserialize<ProcessResult>(json);//vedere risultato con brakpoint

            var output = result.output;

            var exitCode = result.exitCode;
            var pid = result.pid;
            var timestamp = result.timestamp;

            // Salvare in un log input e output della chiamata.
            var log = new Logs();
            log.Output = output;
            log.Pid = pid;
            log.ExitCode = exitCode;
            log.Timestamp = DateTime.UtcNow;
            _context.Logs.Add(log);
            _context.SaveChanges();

            return Content(output);//si utilizza quando una action ritorna una stringa, int, float, ecc... tranne un file o un json
        }


        public IActionResult Stop()
        {
            _tokenSource.Cancel();

            return RedirectToAction("Index", "Jobs");
        }

        [HttpGet/*, ActionName("Run")*/]//perchè risponde alla get 
        public async Task<IActionResult> Run(long id)//jobId è l'id del gruppo cui appartengono i nodi che eseguiranno quel job
        {
            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

            // _context.Jobs è la tabella jobs
            var job = await _context.Jobs.FindAsync(id);//Nella tabella Jobs trova il job == jobId, quindi prende la riga (non la colonna)

            if (job.GroupId == null) { //deve essere l'id del gruppo, non del job
                var r =
                    from n in _context.Nodes
                    where n.Master == false
                    select new { n.Host, n.Port };
                if (job.CronString == null)
                {
                        if (!token.IsCancellationRequested)
                        {
                        foreach (var nodo in r)
                        {//Ottenengo host e porta degli slave appartenti a nodes                 
                            await Process(nodo.Host, nodo.Port, job.Command, job.Arguments, token);
                        }
                    }
                }
                else
                {
                    var schedule = CrontabSchedule.Parse(job.CronString);
                    DateTime endTime = schedule.GetNextOccurrence(DateTime.Now);

                    //System.Timers.Timer timer = new System.Timers.Timer();
                    //timer.Enabled = true;
                    //timer.Start();


                    int secondsRemaining;
                    while (true)
                    {
                        secondsRemaining = (int)(endTime - DateTime.Now).TotalSeconds;
                        if (secondsRemaining == 0)
                        {
                            if (!token.IsCancellationRequested)
                            {
                                foreach (var nodo in r)
                                    await Task.Run(async () =>
                                        {
                                            await Process(nodo.Host, nodo.Port, job.Command, job.Arguments, token);
                                        }, token).ConfigureAwait(false);
                            }
                            break;
                        }
                    }
                }
            }
            else {
                //In GroupsNodes prendi quelli che hanno l'id == job.GroupId
                var nodes = _context.GroupsNodes.Where(p => p.GroupId == job.GroupId);//Ottengo i nodi su cui eseguire il job

                var d =
                    from n in _context.Nodes
                    join gn in _context.GroupsNodes
                    on n.Id equals gn.NodeId
                    where gn.GroupId == job.GroupId
                    select new { n.Host, n.Port };

                if (job.CronString == null)
                {
                        if (!token.IsCancellationRequested)
                        {
                        foreach (var e in d)
                        {
                                await Task.Run(async () =>
                                {
                                    await Process(e.Host, e.Port, job.Command, job.Arguments, token);
                                }, token).ConfigureAwait(false);
                        }
                    }
                }
                else
                {
                    var schedule = CrontabSchedule.Parse(job.CronString);
                    DateTime endTime = schedule.GetNextOccurrence(DateTime.Now);

                    //System.Timers.Timer timer = new System.Timers.Timer();
                    //timer.Enabled = true;
                    //timer.Start();
                    //TimeSpan ts = endTime.Subtract(DateTime.Now);

                    int secondsRemaining;
                    while (true)
                    {
                        secondsRemaining = (int)(endTime - DateTime.Now).TotalSeconds;
                        if (secondsRemaining == 0)
                        {

                            if (!token.IsCancellationRequested)
                            {
                                foreach (var nodo in d)
                                    await Task.Run(async () =>
                                    {
                                        await Process(nodo.Host, nodo.Port, job.Command, job.Arguments, token);
                                    }).ConfigureAwait(false);
                            }
                            break;
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Logs");
        }

        private bool JobsExists(long id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }

        public class ProcessResult
        {
            public string output { get; set; }
            public int exitCode { get; set; }
            public int pid { get; set; }
            public DateTime timestamp { get; set; }
        }
    }
}
