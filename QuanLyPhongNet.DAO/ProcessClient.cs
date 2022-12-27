﻿namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessClient
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;
        string x = "Data Source=msi;Initial Catalog=QuanLyPhongNet;Integrated Security=True";
        public ProcessClient()
        {
            objReader = new QuanLyPhongNETDataContext(x);
            objWriter = new QuanLyPhongNETDataContext(x);
        }

        public List<QuanLyPhongNet.DTO.Client> LoadAllClients()
        {
            return (from client in objReader.Clients
                    select new QuanLyPhongNet.DTO.Client
                    {
                        ClientName=client.ClientName,
                        GroupClientName=client.GroupClientName,
                        Status=client.StatusClient,
                        Note=client.Note
                    }).ToList();
        }

        public void InsertClient(string name,string groupClientName, string status, string note)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                objWriter.Clients.InsertOnSubmit(new Client
                {
                    ClientName = name,
                    GroupClientName = groupClientName,
                    StatusClient = status,
                    Note = note
                });
                objWriter.SubmitChanges();
            }
        }

        public void UpdateClient(string name, string groupClientName, string status, string note)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                Client objUpdate;
                objUpdate = objWriter.Clients.FirstOrDefault(x => x.ClientName.Equals(name));
                objUpdate.ClientName = name;
                objUpdate.GroupClientName = groupClientName;
                objUpdate.StatusClient = status;
                objUpdate.Note = note;
                objWriter.SubmitChanges();
            }
        }
        public void DeleteClient(string clientName)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                var objDelete = objWriter.Clients.Single(x => x.ClientName.Equals(clientName));
                objWriter.Clients.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }
    }
}
