namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProcessMember
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;
        string x = "Data Source=msi;Initial Catalog=QuanLyPhongNet;Integrated Security=True";

        public ProcessMember()
        {
            objReader = new QuanLyPhongNETDataContext(x);
            objWriter = new QuanLyPhongNETDataContext(x);
        }

        public List<QuanLyPhongNet.DTO.Member> LoadAllMembers()
        {
            return (from member in objReader.Members
                    select new QuanLyPhongNet.DTO.Member
                    {
                        AccountName=member.AccountName,
                        Password=member.Password,
                        GroupUserName=member.GroupUser,
                        TimeInAccount=member.CurrentTime.Value,
                        CurrentMoney=(float)member.CurrentMoney,
                        Status=member.StatusAccount
                    }).ToList();             
        }

        public void InsertMember(string account, string pass, string groupUser, TimeSpan time, float money, string status)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                objWriter.Members.InsertOnSubmit(new Member
                {
                    AccountName = account,
                    Password = pass,
                    GroupUser = groupUser,
                    CurrentTime = time,
                    CurrentMoney = money,
                    StatusAccount = status
                });
                objWriter.SubmitChanges();
            }
        }

        public void UpdateMember(int memberID,string account,string pass,string groupUser,TimeSpan time,float money,string status)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                Member objUpdate;
                objUpdate = objWriter.Members.FirstOrDefault(x => x.MemberID == memberID);
                objUpdate.AccountName = account;
                objUpdate.Password = pass;
                objUpdate.GroupUser = groupUser;
                objUpdate.CurrentTime = time;
                objUpdate.CurrentMoney = money;
                objUpdate.StatusAccount = status;
                objWriter.SubmitChanges();
            }
        }

        public void DeleteMember(int memberID)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                var objDelete = objWriter.Members.Single(x => x.MemberID == memberID);
                objWriter.Members.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }
    }
}
