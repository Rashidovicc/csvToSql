using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using SynelProject.Domain.Models;

namespace SynelProject.Service{

    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<Employee> CsvToCollection(IFormFile file)
            {
                StreamReader reader = new StreamReader(file.OpenReadStream());
                CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                var users = csv.GetRecords<Employee>().ToList();
                return users;
            }

            public async Task SaveFileAsync(IFormFile file, string basePath)
            {
                string fileName = Path.Combine("files", Guid.NewGuid() + "_" + file.FileName);
                var stream = System.IO.File.Create(Path.Combine(basePath, fileName));
                await file.CopyToAsync(stream);
                stream.Flush();
            }
    }
}