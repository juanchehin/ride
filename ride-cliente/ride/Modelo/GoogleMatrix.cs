using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace ride.Modelo
  {
  public class GoogleMatrix
    {
    [JsonProperty(PropertyName = "destination_addresses")]
    public string[] DestinationAddresses { get; set; }
    [JsonProperty(PropertyName = "origin_addresses")]
    public string[] OriginAddresses { get; set; }
    public Row[] Rows { get; set; }
    public class Element
      {
      public string status { get; set; }
      public Data duration { get; set; }
      public Data distance { get; set; }

      }
    public class Data
      {
      public int value { get; set; }
      public string text { get; set; }
      }
    public class Row
      {
      public Element[] Elements { get; set; }
      }
    public string Status { get; set; }
    }
  }
