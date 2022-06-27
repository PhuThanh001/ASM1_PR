
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObject;


namespace DataAccess
{
    public class MemberDAO
    {
        private static List<MemberObject> MemberList = new List<MemberObject>()
        {
            new MemberObject {MemberID = "PhuThanh", Password="1", Email="phuntse151054@fpt.edu.vn",
            City="HaNoi", Country="Vietnam", MemberName = "Thanh phu"},

            new MemberObject {MemberID = "A san", Password="1", Email="Asanwa@gmail.com",
            City="SaiGon", Country="Vietnam", MemberName = "A"},

            new MemberObject {MemberID = "Tri Cong", Password="1", Email="tricong@gmail.com",
            City="SaiGon", Country="Vietnam", MemberName = "Cong"},

            new MemberObject {MemberID = "Ducsan", Password="1", Email="ducsanwa@gmail.com",
            City="SaiGon", Country="Vietnam", MemberName = "Duc"},

            new MemberObject {MemberID = "abcdu", Password="1", Email="example@gmail.com",
            City="HaNoi", Country="Vietnam", MemberName = "Tuan 0"}
        };

        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<MemberObject> GetMemberObjectsList => MemberList;

        public MemberObject GetMemberByMemberID(string memberID)
        {
            MemberObject memberObject = MemberList.SingleOrDefault(mem => mem.MemberID == memberID);
            return memberObject;
        }
        
        //Add a new member
        public void AddNewMember(MemberObject addMem)
        {
            MemberObject mem = GetMemberByMemberID(addMem.MemberID);
            if(mem == null)
            {
                if (CheckEmailDuplicated(addMem.Email))
                {
                    throw new Exception("Email is already used!");
                }
                else
                {
                    MemberList.Add(addMem);
                }
                
            }
            else if (mem != null)
            {
                throw new Exception("Member is already exist!");
            }
        }

        //Update a member

        public void UpdateAMember(MemberObject updateMem)
        {
            MemberObject mem = GetMemberByMemberID(updateMem.MemberID);
            if(mem != null)
            {
                if (CheckUpdateEmailDuplicated(updateMem.MemberID, updateMem.Email))
                {
                    throw new Exception("Email is already used!");
                }
                var index = MemberList.IndexOf(mem);
                MemberList[index] = updateMem;
            }
            else
            {
                throw new Exception("Member doesn't exist!");
            }
        }

        //Remove a member

        public void RemoveAMember(string memberID)
        {
            MemberObject mem = GetMemberByMemberID(memberID);
            if (mem != null)
            {
                MemberList.Remove(mem);
            }
            else
            {
                throw new Exception("Member doesn't exist!");
            }
        }

        public MemberObject GetMemberByEmail(string email)
        {

            MemberObject memberObject = MemberList.SingleOrDefault(mem => mem.Email == email);
            return memberObject;
        }

        public Boolean CheckEmailDuplicated(string email)
        {
            Boolean check;
            MemberObject memberObject = MemberList.SingleOrDefault(mem => mem.Email == email);
            if (memberObject == null)
            {
                check = false;
            }
            else
            {
                check = true;
            }
            return check;
        }

        public Boolean CheckUpdateEmailDuplicated(string userID, string email)
        {
            Boolean check;
            MemberObject memberObject = MemberList.SingleOrDefault(mem => mem.Email == email);
            if (memberObject == null)
            {
                check = false;
            }
            else
            {
                if (memberObject.MemberID == userID)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            return check;
        }

        public List <MemberObject> SearchMemberByName(string memberName)
        {
            return (List<MemberObject>)MemberList.Where(m => m.MemberName.Contains(memberName)).ToList(); 
        }

        public List<MemberObject> SearchMemberByID(string memberID)
        {
            return (List<MemberObject>)MemberList.Where(m => m.MemberID.Contains(memberID)).ToList();
        }

        public List<MemberObject> FilterMemberByCity(string city)
        {
            return (List<MemberObject>)MemberList.Where(m => m.City.Equals(city)).ToList();
        }

        public List<MemberObject> FilterMemberByCountry(string country)
        {
            return (List<MemberObject>)MemberList.Where(m => m.Country.Equals(country)).ToList();
        }

    }
}
