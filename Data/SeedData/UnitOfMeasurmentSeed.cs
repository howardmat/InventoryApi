using Data.Enums;
using Data.Models;
using System;

namespace Data.SeedData
{
    public static class UnitOfMeasurementSeed
    {
        public static UnitOfMeasurement[] Data = {
            // Metric Volume
            new UnitOfMeasurement
            {
                Id = 1,
                Name = "Millilitres",
                Abbreviation = "mL",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 2,
                Name = "Litres",
                Abbreviation = "L",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Metric Weight
            new UnitOfMeasurement
            {
                Id = 3,
                Name = "Milligrams",
                Abbreviation = "mg",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Weight,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 4,
                Name = "Grams",
                Abbreviation = "g",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Weight,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 5,
                Name = "Kilograms",
                Abbreviation = "kg",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Weight,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Metric Length
            new UnitOfMeasurement
            {
                Id = 6,
                Name = "millimetres",
                Abbreviation = "mm",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Length,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 7,
                Name = "centimetres",
                Abbreviation = "cm",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Length,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 8,
                Name = "metres",
                Abbreviation = "m",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Length,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Metric Area
            new UnitOfMeasurement
            {
                Id = 9,
                Name = "square millimetres",
                Abbreviation = "mm&sup2;",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Area,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 10,
                Name = "square centimetres",
                Abbreviation = "cm&sup2;",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Area,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 11,
                Name = "square metres",
                Abbreviation = "m&sup2;",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Area,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Metric Cubic
            new UnitOfMeasurement
            {
                Id = 12,
                Name = "cubic millimetres",
                Abbreviation = "mm&#179;",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Cubic,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 13,
                Name = "cubic centimetres",
                Abbreviation = "cm&#179;",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Cubic,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 14,
                Name = "cubic metres",
                Abbreviation = "m&#179;",
                System = MeasurementSystem.Metric,
                Type = MeasurementType.Cubic,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Imperial Volume
            new UnitOfMeasurement
            {
                Id = 15,
                Name = "Fluid Ounces",
                Abbreviation = "fl oz",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 16,
                Name = "Pint",
                Abbreviation = "pt",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 17,
                Name = "Quart",
                Abbreviation = "qt",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 18,
                Name = "Gallon",
                Abbreviation = "gal",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Imperial Weights
            new UnitOfMeasurement
            {
                Id = 19,
                Name = "Grain",
                Abbreviation = "gr",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Weight,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 20,
                Name = "Dram",
                Abbreviation = "dr",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Weight,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 21,
                Name = "Ounce",
                Abbreviation = "oz",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Weight,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 22,
                Name = "Pounds",
                Abbreviation = "lb",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Weight,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Imperial Length
            new UnitOfMeasurement
            {
                Id = 23,
                Name = "Inch",
                Abbreviation = "in",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Length,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 24,
                Name = "Feet",
                Abbreviation = "ft",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Length,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 25,
                Name = "Yard",
                Abbreviation = "yd",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Length,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Imperial Area
            new UnitOfMeasurement
            {
                Id = 26,
                Name = "Square Inch",
                Abbreviation = "in&sup2;",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Area,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 27,
                Name = "Square Feet",
                Abbreviation = "ft&sup2;",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Area,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 28,
                Name = "Square Yard",
                Abbreviation = "yd&sup2;",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Area,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Imperial Cubic
            new UnitOfMeasurement
            {
                Id = 29,
                Name = "Cubic Inch",
                Abbreviation = "in&#179;",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Cubic,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 30,
                Name = "Cubic Feet",
                Abbreviation = "ft&#179;",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Cubic,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 31,
                Name = "Cubic Yard",
                Abbreviation = "yd&#179;",
                System = MeasurementSystem.Imperial,
                Type = MeasurementType.Cubic,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Count measurements
            new UnitOfMeasurement
            {
                Id = 32,
                Name = "Unit",
                Abbreviation = "unit",
                System = MeasurementSystem.Other,
                Type = MeasurementType.Count,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 33,
                Name = "Chip",
                Abbreviation = "chip",
                System = MeasurementSystem.Other,
                Type = MeasurementType.Count,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 34,
                Name = "Drop",
                Abbreviation = "drop",
                System = MeasurementSystem.Other,
                Type = MeasurementType.Count,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },

            // Cooking Volumes
            new UnitOfMeasurement
            {
                Id = 35,
                Name = "Drop",
                Abbreviation = "dr",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 36,
                Name = "Smidgen",
                Abbreviation = "smdg",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 37,
                Name = "Pinch",
                Abbreviation = "pn",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 38,
                Name = "Dash",
                Abbreviation = "ds",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 39,
                Name = "Teaspoon",
                Abbreviation = "tsp",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 40,
                Name = "Tablespoon",
                Abbreviation = "tbsp",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 41,
                Name = "Fluid Ounce",
                Abbreviation = "fl oz",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 42,
                Name = "Cup",
                Abbreviation = "C",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 43,
                Name = "Pint",
                Abbreviation = "pt",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
            new UnitOfMeasurement
            {
                Id = 44,
                Name = "Quart",
                Abbreviation = "qt",
                System = MeasurementSystem.Cooking,
                Type = MeasurementType.Volume,
                CreatedUserId = null,
                CreatedUtc = new DateTime(2020, 1, 1),
                LastModifiedUserId = null,
                LastModifiedUtc = new DateTime(2020, 1, 1)
            },
        };
    }
}
