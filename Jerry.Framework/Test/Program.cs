﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Log;
using Jerry.System.RabbitMq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Test
{
    class Program
    {
        private static ILog log = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {

            PersonList pList = new PersonList();

            foreach (PersonYield item in pList)
            {
                item.SayHi();
            }
            Console.ReadLine();
            return;
            Person[] peopleArray = new Person[3]
          {
                           new Person("John", "Smith"),
              new Person("Jim", "Johnson"),
              new Person("Sue", "Rabon"),
          };



            People peopleList = new People(peopleArray);
            while (peopleList.GetEnumerator().MoveNext())
            {

            }
            foreach (Person p in peopleList)
                Console.WriteLine(p.firstName + " " + p.lastName);

            //for (int i = 0; i < 100; i++)
            //{
            //    RabbitMqMessage message = new RabbitMqMessage()
            //    {
            //        Message = i + "号消息",
            //        IsOperationOk = false
            //    };
            //    RabbitMqContext.Instance.PublishMessage(message);
            //}
            //RabbitMqContext.Instance.Dispose();
            //RabbitMqContext.Instance.Connect();
            //RabbitMqContext.Instance.ActionEventMessage += Instance_ActionEventMessage;
            //RabbitMqContext.Instance.Receive();
            Console.Read();
        }

        private static void Instance_ActionEventMessage(RabbitMqMessage message)
        {
            message.IsOperationOk = true;
            message.ReceiveTime = DateTime.Now;
            Console.WriteLine(" [x] Received {0}", message.Message);
            log.Info("接受到消息：" + JsonConvert.SerializeObject(message));
        }
    }
}
