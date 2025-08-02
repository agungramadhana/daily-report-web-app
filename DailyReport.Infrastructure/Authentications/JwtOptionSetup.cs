using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DailyReport.Infrastructure.Authentications
{
    public class JwtOptionSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;
        private const string ConfigName = "jwt";

        public JwtOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(ConfigName).Bind(options);
        }
    }
}
