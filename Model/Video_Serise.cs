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
    /// 实体类Video_Serise。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("Video_Serise")]
    [Serializable]
    public partial class Video_Serise : Entity
    {
        #region Model
        private int? _id;
        private string _SeriseName;
        private int? _Cnt;

        /// <summary>
        /// 
        /// </summary>
        public int? id
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
        public int? Cnt
        {
            get { return _Cnt; }
            set
            {
                this.OnPropertyValueChange("Cnt");
                this._Cnt = value;
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
            };
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
                _.id,
                _.SeriseName,
                _.Cnt,
            };
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
                this._id,
                this._SeriseName,
                this._Cnt,
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
            public readonly static Field All = new Field("*", "Video_Serise");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field id = new Field("id", "Video_Serise", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field SeriseName = new Field("SeriseName", "Video_Serise", "");
            /// <summary>
			/// 
			/// </summary>
			public readonly static Field Cnt = new Field("Cnt", "Video_Serise", "");
        }
        #endregion
    }
}