
using SynelProject.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;

namespace SynelProject.Service
{

    public interface IEmployeeService
    {
        Task SaveFileAsync(IFormFile file, string basePath);
        IEnumerable<Employee> CsvToCollection(IFormFile file);
    }
}