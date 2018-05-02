﻿using HaloSharp.Exception;
using HaloSharp.Model;
using HaloSharp.Query.Metadata;

namespace HaloSharp.Validation.Metadata
{
    public static class GetRequisitionPackValidator
    {
        public static void Validate(this GetRequisitionPack getRequisitionPack)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrWhiteSpace(getRequisitionPack.Id))
            {
                validationResult.Messages.Add("GetRequisitionPack query requires a RequisitionPackId to be set.");
            }

            if (!validationResult.Success)
            {
                throw new ValidationException(validationResult.Messages);
            }
        }
    }
}
