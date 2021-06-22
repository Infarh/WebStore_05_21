using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат (Либо русские, либо латинские символы начиная с заглавной буквы)")]
        public string LastName { get; set; }

        [Display(Name = "Имя")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Строка имела неверный формат (Либо русские, либо латинские символы начиная с заглавной буквы)")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        [StringLength(200, ErrorMessage = "Длина имени должна быть до 200 символов")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Возраст должен быть от 18 до 80 лет")]
        public int Age { get; set; }
    }
}
