// <copyright file="ICrudFunctions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Interface
#pragma warning restore SA1652 // Enable XML documentation output
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// CRUD Interface:
    /// The software must allow the user to
    /// - Create or add new entries
    /// - Read, retrieve, search, or view existing entries
    /// - Update or edit existing entries
    /// - Delete/deactivate/remove existing entries
    /// These functionalities is provided by the ICrudFunctions interface.
    /// </summary>
    public interface ICrudFunctions
    {
        void CreateEntry(object obj);

        object SearchEntry(object obj);

        List<object> ListAllEntry();

        void EditEntry(object obj);

        void DeleteEntry(object obj);
    }
}
