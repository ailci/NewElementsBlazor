using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Shared.ViewModels;

public class AuthorForCreateViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly? BirthDate { get; set; }
    public IBrowserFile? Photo { get; set; }
}