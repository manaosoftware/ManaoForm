﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Core.ValidationHandler.Base
{
    public class ValidationFailure
    {
        public object AttemptedValue { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
