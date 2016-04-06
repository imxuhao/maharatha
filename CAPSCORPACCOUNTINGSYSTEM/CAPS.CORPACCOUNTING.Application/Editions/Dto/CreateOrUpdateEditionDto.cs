﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Editions.Dto
{
    public class CreateOrUpdateEditionDto : IInputDto
    {
        [Required]
        public EditionEditDto Edition { get; set; }

       // [Required]
        public List<NameValueDto> FeatureValues { get; set; }
    }
}