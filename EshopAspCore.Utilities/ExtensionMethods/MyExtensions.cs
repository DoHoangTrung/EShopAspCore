﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EshopAspCore.Utilities.ExtensionMethods
{
    public static class MyExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static string ToVNDString(this decimal value)
        {
            return value.ToString("C", CultureInfo.CreateSpecificCulture("vi-VN"));
        }
    }
}
