using System;
using System.Collections.Generic;
using System.Text;

public class API_Element_Request
{
    public string Name { get; set; }
    public string Method { get; set; }
    public string URL { get; set; }
}

public class API_Request
{
    public List<API_Element_Request> APIs { get; set; }
}