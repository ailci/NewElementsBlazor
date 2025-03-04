﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Validations;

namespace Shared.ViewModels;

public class AuthorForCreateViewModel
{
    [Required(ErrorMessage = "Bitte geben Sie einen Namen ein")]
    [Length(2, 100, ErrorMessage = "Name ist zu lang bzw. zu kurz")]
    [DeniedValues(["administrator","admin","root","god"], ErrorMessage = "Der Name ist nicht erlaubt")]
    public string Name { get; set; } = string.Empty;

    [MinLength(2, ErrorMessage = "Bitte geben Sie eine Beschreibung mit mind. 2 Zeichen ein")]
    public string Description { get; set; } = string.Empty;

    [NoFutureDate(ErrorMessage = "Geburtsdatum liegt in der Zukunft")]
    public DateOnly? BirthDate { get; set; }

    [AllowedFileFormats("jpg","jpeg","png")]
    public IBrowserFile? Photo { get; set; }
}