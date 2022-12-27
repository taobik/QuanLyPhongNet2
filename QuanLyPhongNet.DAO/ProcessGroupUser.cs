﻿namespace QuanLyPhongNet.DAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using QuanLyPhongNet.Common;
    using QuanLyPhongNet.DTO;

    public class ProcessGroupUser
    {
        private QuanLyPhongNETDataContext objReader;
        private QuanLyPhongNETDataContext objWriter;
        string x = "Data Source=msi;Initial Catalog=QuanLyPhongNet;Integrated Security=True";
        public ProcessGroupUser()
        {
            objReader = new QuanLyPhongNETDataContext(x);
            objWriter = new QuanLyPhongNETDataContext(x);
        }

        public List<QuanLyPhongNet.DTO.GroupUser> LoadAllGroupUsers()
        {
            return (from groupUser in objReader.GroupUsers
                    select new QuanLyPhongNet.DTO.GroupUser
                    {
                        GroupUserName=groupUser.GroupName,
                        TypeName=groupUser.TypeName
                    }).ToList();
        }

        public void UpdateGroupUser(string groupUserName, string typeName)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                GroupUser objUpdate;
                if ((objUpdate = objWriter.GroupUsers.FirstOrDefault(x => x.GroupName.Equals(groupUserName))) == null)
                {
                    objWriter.GroupUsers.InsertOnSubmit(new GroupUser
                    {
                        GroupName = groupUserName,
                        TypeName = typeName
                    });
                    objWriter.SubmitChanges();
                }
                else
                {
                    objUpdate.GroupName = groupUserName;
                    objUpdate.TypeName = typeName;
                    objWriter.SubmitChanges();
                }
            }
        }

        public void DeleteGroupUser(string groupUserName)
        {
            using (QuanLyPhongNETDataContext objWriter = new QuanLyPhongNETDataContext(x))
            {
                var objDelete = objWriter.GroupUsers.Single(x => x.GroupName.Equals(groupUserName));
                objWriter.GroupUsers.DeleteOnSubmit(objDelete);
                objWriter.SubmitChanges();
            }
        }
    }
}
