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
using System.Data;
using System.Data.Common;
using Dos.ORM;
using Dos.ORM.Common;

namespace WebGather.Model
{

	/// <summary>
	/// 实体类Alibaba_ProInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Table("Alibaba_ProInfo")]
	[Serializable]
	public partial class Alibaba_ProInfo : Entity 
	{
		#region Model
		private int _Id;
		private long? _AliProductId;
		private long? _AliGroupId;
		private string _AliGroupName;
		private int? _CompanyId;
		private string _Title;
		private string _Detail;
		private string _ProNo;
		private string _FirstPic;
		private string _Pics;
		private string _PriceRange;
		private decimal? _MaxPrice;
		private decimal? _MinPrice;
		private int? _Moq;
		private string _MoqStr;
		private string _Attribute;
		private string _SupplyAbility;
		private string _Port;
		private string _PaymentTerms;
		private string _PackagingDetails;
		private string _DeliveryDetail;
		private DateTime? _AddTime;
		private int? _Tag;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			get{ return _Id; }
			set
			{
				this.OnPropertyValueChange(_.Id,_Id,value);
				this._Id=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? AliProductId
		{
			get{ return _AliProductId; }
			set
			{
				this.OnPropertyValueChange(_.AliProductId,_AliProductId,value);
				this._AliProductId=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? AliGroupId
		{
			get{ return _AliGroupId; }
			set
			{
				this.OnPropertyValueChange(_.AliGroupId,_AliGroupId,value);
				this._AliGroupId=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AliGroupName
		{
			get{ return _AliGroupName; }
			set
			{
				this.OnPropertyValueChange(_.AliGroupName,_AliGroupName,value);
				this._AliGroupName=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CompanyId
		{
			get{ return _CompanyId; }
			set
			{
				this.OnPropertyValueChange(_.CompanyId,_CompanyId,value);
				this._CompanyId=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			get{ return _Title; }
			set
			{
				this.OnPropertyValueChange(_.Title,_Title,value);
				this._Title=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Detail
		{
			get{ return _Detail; }
			set
			{
				this.OnPropertyValueChange(_.Detail,_Detail,value);
				this._Detail=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProNo
		{
			get{ return _ProNo; }
			set
			{
				this.OnPropertyValueChange(_.ProNo,_ProNo,value);
				this._ProNo=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FirstPic
		{
			get{ return _FirstPic; }
			set
			{
				this.OnPropertyValueChange(_.FirstPic,_FirstPic,value);
				this._FirstPic=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pics
		{
			get{ return _Pics; }
			set
			{
				this.OnPropertyValueChange(_.Pics,_Pics,value);
				this._Pics=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PriceRange
		{
			get{ return _PriceRange; }
			set
			{
				this.OnPropertyValueChange(_.PriceRange,_PriceRange,value);
				this._PriceRange=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MaxPrice
		{
			get{ return _MaxPrice; }
			set
			{
				this.OnPropertyValueChange(_.MaxPrice,_MaxPrice,value);
				this._MaxPrice=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? MinPrice
		{
			get{ return _MinPrice; }
			set
			{
				this.OnPropertyValueChange(_.MinPrice,_MinPrice,value);
				this._MinPrice=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Moq
		{
			get{ return _Moq; }
			set
			{
				this.OnPropertyValueChange(_.Moq,_Moq,value);
				this._Moq=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MoqStr
		{
			get{ return _MoqStr; }
			set
			{
				this.OnPropertyValueChange(_.MoqStr,_MoqStr,value);
				this._MoqStr=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Attribute
		{
			get{ return _Attribute; }
			set
			{
				this.OnPropertyValueChange(_.Attribute,_Attribute,value);
				this._Attribute=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplyAbility
		{
			get{ return _SupplyAbility; }
			set
			{
				this.OnPropertyValueChange(_.SupplyAbility,_SupplyAbility,value);
				this._SupplyAbility=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Port
		{
			get{ return _Port; }
			set
			{
				this.OnPropertyValueChange(_.Port,_Port,value);
				this._Port=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PaymentTerms
		{
			get{ return _PaymentTerms; }
			set
			{
				this.OnPropertyValueChange(_.PaymentTerms,_PaymentTerms,value);
				this._PaymentTerms=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PackagingDetails
		{
			get{ return _PackagingDetails; }
			set
			{
				this.OnPropertyValueChange(_.PackagingDetails,_PackagingDetails,value);
				this._PackagingDetails=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DeliveryDetail
		{
			get{ return _DeliveryDetail; }
			set
			{
				this.OnPropertyValueChange(_.DeliveryDetail,_DeliveryDetail,value);
				this._DeliveryDetail=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AddTime
		{
			get{ return _AddTime; }
			set
			{
				this.OnPropertyValueChange(_.AddTime,_AddTime,value);
				this._AddTime=value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Tag
		{
			get{ return _Tag; }
			set
			{
				this.OnPropertyValueChange(_.Tag,_Tag,value);
				this._Tag=value;
			}
		}
		#endregion

		#region Method
		/// <summary>
		/// 获取实体中的标识列
		/// </summary>
		public override Field GetIdentityField()
		{
			return _.Id;
		}
		/// <summary>
		/// 获取实体中的主键列
		/// </summary>
		public override Field[] GetPrimaryKeyFields()
		{
			return new Field[] {
				_.Id};
		}
		/// <summary>
		/// 获取列信息
		/// </summary>
		public override Field[] GetFields()
		{
			return new Field[] {
				_.Id,
				_.AliProductId,
				_.AliGroupId,
				_.AliGroupName,
				_.CompanyId,
				_.Title,
				_.Detail,
				_.ProNo,
				_.FirstPic,
				_.Pics,
				_.PriceRange,
				_.MaxPrice,
				_.MinPrice,
				_.Moq,
				_.MoqStr,
				_.Attribute,
				_.SupplyAbility,
				_.Port,
				_.PaymentTerms,
				_.PackagingDetails,
				_.DeliveryDetail,
				_.AddTime,
				_.Tag};
		}
		/// <summary>
		/// 获取值信息
		/// </summary>
		public override object[] GetValues()
		{
			return new object[] {
				this._Id,
				this._AliProductId,
				this._AliGroupId,
				this._AliGroupName,
				this._CompanyId,
				this._Title,
				this._Detail,
				this._ProNo,
				this._FirstPic,
				this._Pics,
				this._PriceRange,
				this._MaxPrice,
				this._MinPrice,
				this._Moq,
				this._MoqStr,
				this._Attribute,
				this._SupplyAbility,
				this._Port,
				this._PaymentTerms,
				this._PackagingDetails,
				this._DeliveryDetail,
				this._AddTime,
				this._Tag};
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
			public readonly static Field All = new Field("*","Alibaba_ProInfo");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Id = new Field("Id","Alibaba_ProInfo","Id");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AliProductId = new Field("AliProductId","Alibaba_ProInfo","AliProductId");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AliGroupId = new Field("AliGroupId","Alibaba_ProInfo","AliGroupId");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AliGroupName = new Field("AliGroupName","Alibaba_ProInfo","AliGroupName");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field CompanyId = new Field("CompanyId","Alibaba_ProInfo","CompanyId");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Title = new Field("Title","Alibaba_ProInfo","Title");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Detail = new Field("Detail","Alibaba_ProInfo","Detail");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field ProNo = new Field("ProNo","Alibaba_ProInfo","ProNo");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field FirstPic = new Field("FirstPic","Alibaba_ProInfo","FirstPic");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Pics = new Field("Pics","Alibaba_ProInfo","Pics");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field PriceRange = new Field("PriceRange","Alibaba_ProInfo","PriceRange");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field MaxPrice = new Field("MaxPrice","Alibaba_ProInfo","MaxPrice");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field MinPrice = new Field("MinPrice","Alibaba_ProInfo","MinPrice");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Moq = new Field("Moq","Alibaba_ProInfo","Moq");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field MoqStr = new Field("MoqStr","Alibaba_ProInfo","MoqStr");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Attribute = new Field("Attribute","Alibaba_ProInfo","Attribute");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field SupplyAbility = new Field("SupplyAbility","Alibaba_ProInfo","SupplyAbility");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Port = new Field("Port","Alibaba_ProInfo","Port");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field PaymentTerms = new Field("PaymentTerms","Alibaba_ProInfo","PaymentTerms");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field PackagingDetails = new Field("PackagingDetails","Alibaba_ProInfo","PackagingDetails");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field DeliveryDetail = new Field("DeliveryDetail","Alibaba_ProInfo","DeliveryDetail");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field AddTime = new Field("AddTime","Alibaba_ProInfo","AddTime");
			/// <summary>
			/// 
			/// </summary>
			public readonly static Field Tag = new Field("Tag","Alibaba_ProInfo","Tag");
		}
		#endregion


	}
}

