namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using QuanLyPhongNet.Common;
    using QuanLyPhongNet.DTO;

    public class ProcessGroupClient
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;
        string x = "Data Source=msi;Initial Catalog=QuanLyPhongNet;Integrated Security=True";

        public ProcessGroupClient()
        {
            objReader = new QuanLyPhongNETDataContext(x);
            objWriter = new QuanLyPhongNETDataContext(x);
        }

        public List<QuanLyPhongNet.DTO.GroupClient> LoadAllGroupClients()
        {
            return (from groupClient in objReader.GroupClients
                    select new QuanLyPhongNet.DTO.GroupClient
                    {
                        GroupClientName=groupClient.GroupName,
                        Description=groupClient.Discription,
                    }).ToList();
        }

        public void InsertGroupClient(string groupClientName, string description)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                objWriter.GroupClients.InsertOnSubmit(new GroupClient
                {
                    GroupName = groupClientName,
                    Discription = description
                });
                objWriter.SubmitChanges();
            }
        }

        public void UpdateGroupClient(string groupClientName,string description)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                GroupClient objUpdate;
                objUpdate = objWriter.GroupClients.FirstOrDefault(x => x.GroupName.Equals(groupClientName));
                objUpdate.GroupName = groupClientName;
                objUpdate.Discription = description;
                objWriter.SubmitChanges();
            }
        }

        public void DeleteGroupClient(string groupClientName)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                var objDelete = objWriter.GroupClients.Single(x => x.GroupName.Equals(groupClientName));
                objWriter.GroupClients.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }
    }
}
