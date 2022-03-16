using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Slave.ViewModels;

namespace Slave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProcessController : ControllerBase
    {
        [HttpGet]
        public ProcessResult Exec(string fileName, string arguments)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arguments;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                var response = new ProcessResult
                {
                    output = process.StandardOutput.ReadToEnd(),
                    pid = process.Id,
                    exitCode = process.ExitCode,
                };

                process.WaitForExit();

                return response;
            }
        }
    }
}
