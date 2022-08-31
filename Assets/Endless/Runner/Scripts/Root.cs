using System.Collections.Generic;
using System;

namespace Endless.Runner
{

    public class Root
    {
        public int total { get; set; }
        public int page { get; set; }
        public int page_size { get; set; }
        public List<Result> result { get; set; }
    }

    public class Result
    {
        public string hash { get; set; }
        public string nonce { get; set; }
        public string transaction_index { get; set; }
        public string from_address { get; set; }
        public string to_address { get; set; }
        public string value { get; set; }
        public string gas { get; set; }
        public string gas_price { get; set; }
        public string input { get; set; }
        public string receipt_cumulative_gas_used { get; set; }
        public string receipt_gas_used { get; set; }
        public object receipt_contract_address { get; set; }
        public object receipt_root { get; set; }
        public string receipt_status { get; set; }
        public DateTime block_timestamp { get; set; }
        public string block_number { get; set; }
        public string block_hash { get; set; }
    }



    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
}