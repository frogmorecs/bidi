﻿using System.Collections.Generic;
using CommandLine;

namespace Test
{
    public class Options
    {
        [Option('p', "printername", Required = true, HelpText = "Name of the printer to query.")]
        public string PrinterName { get; set; }

        [Option('s', 
            "schemas", 
            Required = false, 
            HelpText = "Schemas to request from the printer (comma separated).", 
            Default = new []{"\\Printer"},
            Separator = ',')]
        public IEnumerable<string> Schemas { get; set; }

        [Option('a', "admin", Required = false, HelpText = "Request Administrator access.")]
        public bool Admin { get; set; } = false;
    }

    [Verb("Get", HelpText = "Get the value of a specified schema.")]
    public class GetOptions : Options
    {

    }

    [Verb("GetWithArgument", HelpText = "Request the bidi schema value using the data set as input argument.")]
    public class GetWithArgumentOptions : Options
    {

    }

    [Verb("GetAll", HelpText = "Get the values of all child nodes of the specified schema.")]
    public class GetAllOptions : Options
    {

    }

    [Verb("Set", HelpText = "Set a value of the schema.")]
    public class SetOptions : Options
    {

    }

    [Verb("EnumSchema", HelpText = "Enumerate the schema. The returned data will be a list of schema that the port monitor or print provider supports.")]
    public class EnumSchemaOptions : Options
    {

    }
}