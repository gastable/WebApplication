using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _04ViewModel.Models;
using _04ViewModel.ViewModels;

namespace _04ViewModel.ViewModels
{
    public class EmpDept
    {
        public List<tDepartment> department { get; set; }
        public List<tEmployee> employee { get; set; }
    }
}