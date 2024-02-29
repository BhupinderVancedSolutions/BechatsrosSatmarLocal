﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Response.Transactions
{
    public class TransactionResponseDto
    {
        public int TransactionId { get; set; }
        public string UserId { get; set; }
       
        public decimal Amount { get; set; }
        public string TransactionResult { get; set; }
        public string TransactionGuid { get; set; }          
        public string TransactionType { get; set; }       
        public int? PaymentMethodId { get; set; }
        public int? ItemTypeId { get; set; }      
        public int? CCProcessorId { get; set; }             
        public string Reason { get; set; }
      
       
      
       
       
      
               
    }
}