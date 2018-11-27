﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Dos.ORM;

namespace WebGather.Model
{
    /// <summary>
    /// 实体类Video_Info。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Video_Info")]
    [Serializable]
    public partial class Video_Info : Entity
    {
        #region Model
        private int _id;
        private int? _ClassId;
        private string _ClassPath;
        private string _VideoNo;
        private int? _SeriseId;
        private string _SeriseName;
        private string _Title;
        private string _Content;
        private string _FirstPic;
        private string _Pics;
        private string _Author;
        private string _Keywords;
        private bool? _IsShow;
        private int? _Hits;
        private DateTime? _UpdateTime;
        private DateTime? _AddTime;

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            get { return _id; }
            set
            {
                this.OnPropertyValueChange("id");
                this._id = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ClassId
        {
            get { return _ClassId; }
            set
            {
                this.OnPropertyValueChange("ClassId");
                this._ClassId = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassPath
        {
            get { return _ClassPath; }
            set
            {
                this.OnPropertyValueChange("ClassPath");
                this._ClassPath = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VideoNo
        {
            get { return _VideoNo; }
            set
            {
                this.OnPropertyValueChange("VideoNo");
                this._VideoNo = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SeriseId
        {
            get { return _SeriseId; }
            set
            {
                this.OnPropertyValueChange("SeriseId");
                this._SeriseId = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SeriseName
        {
            get { return _SeriseName; }
            set
            {
                this.OnPropertyValueChange("SeriseName");
                this._SeriseName = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                this.OnPropertyValueChange("Title");
                this._Title = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set
            {
                this.OnPropertyValueChange("Content");
                this._Content = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FirstPic
        {
            get { return _FirstPic; }
            set
            {
                this.OnPropertyValueChange("FirstPic");
                this._FirstPic = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pics
        {
            get { return _Pics; }
            set
            {
                this.OnPropertyValueChange("Pics");
                this._Pics = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Author
        {
            get { return _Author; }
            set
            {
                this.OnPropertyValueChange("Author");
                this._Author = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Keywords
        {
            get { return _Keywords; }
            set
            {
                this.OnPropertyValueChange("Keywords");
                this._Keywords = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsShow
        {
            get { return _IsShow; }
            set
            {
                this.OnPropertyValueChange("IsShow");
                this._IsShow = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Hits
        {
            get { return _Hits; }
            set
            {
                this.OnPropertyValueChange("Hits");
                this._Hits = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            get { return _UpdateTime; }
            set
            {
                this.OnPropertyValueChange("UpdateTime");
                this._UpdateTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? AddTime
        {
            get { return _AddTime; }
            set
            {
                this.OnPropertyValueChange("AddTime");
                this._AddTime = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
                _.id,
            };
        }
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.id;
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
                _.id,
                _.ClassId,
                _.ClassPath,
                _.VideoNo,
                _.SeriseId,
                _.SeriseName,
                _.Title,
                _.Content,
                _.FirstPic,
                _.Pics,
                _.Author,
                _.Keywords,
                _.IsShow,
                _.Hits,
                _.UpdateTime,
                _.AddTime,
            };
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
                this._id,
                this._ClassId,
                this._ClassPath,
                this._VideoNo,
                this._SeriseId,
                this._SeriseName,
                this._Title,
                this._Content,
                this._FirstPic,
                this._Pics,
                this._Author,
                this._Keywords,
                this._IsShow,
                this._Hits,
                this._UpdateTime,
                this._AddTime,
            };
        }
        /// <summary>
        /// 是否是v1.10.5.6及以上版本实体。
        /// </summary>
        /// <returns></returns>
        public override bool V1_10_5_6_Plus()
        {
            return true;
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public readonly static Field All = new Field("*", "Video_Info");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ClassId = new Field("ClassId", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field ClassPath = new Field("ClassPath", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field VideoNo = new Field("VideoNo", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field SeriseId = new Field("SeriseId", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field SeriseName = new Field("SeriseName", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Title = new Field("Title", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Content = new Field("Content", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field FirstPic = new Field("FirstPic", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Pics = new Field("Pics", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Author = new Field("Author", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Keywords = new Field("Keywords", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field IsShow = new Field("IsShow", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Hits = new Field("Hits", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field UpdateTime = new Field("UpdateTime", "Video_Info", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field AddTime = new Field("AddTime", "Video_Info", "");
        }
        #endregion
    }
}