using Core.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
	public static class Messages
	{
		public static string ProductCountOfCategoryError = "Kategoride en fazla 10 ürün olabilir";
		public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
		public static string CategoryLimitExceeded = "Bu kategoriye daha fazla ürün eklenemez";
		public static string? AuthorizationDenied = "Yetkiniz yok!";
		public static string UserRegistered = "Kayıt oldu.";
		public static string UserNotFound = "Kullanıcı bulunamadı";
		public static string PasswordError = "Parola hatası";
		public static string SuccessfulLogin = "Başarılı giriş";
		public static string UserAlreadyExists = "Kullanıcı mevcut";
		public static string AccessTokenCreated = "Token oluşturuldu";
	}
}
