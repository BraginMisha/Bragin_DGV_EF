using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace DataGridView
{
    public static class Extensions
    {
        /// <summary>
        /// Добавляет привязку данных (`Binding`) между свойством контрола и свойством объекта модели.
        /// Автоматически отслеживает изменения данных в модели и обновляет контрол.
        /// Также, если передан `ErrorProvider`, добавляет валидацию модели с использованием атрибутов данных
        /// (например, `Required`, `Range`, и т.д.) и отображает ошибки валидации на контроле.
        /// </summary>
        /// <typeparam name="TControl">Тип контрола (например, `TextBox`, `NumericUpDown`, и т.д.).</typeparam>
        /// <typeparam name="TSource">Тип модели данных, к которой привязывается контрол.</typeparam>
        /// <param name="target">Контрол, к которому привязывается свойство модели.</param>
        /// <param name="targetProperty">Выражение для указания свойства контрола, которое будет привязано.</param>
        /// <param name="source">Модель данных, содержащая привязываемое свойство.</param>
        /// <param name="sourceProperty">Выражение для указания свойства модели, которое будет привязано.</param>
        /// <param name="errorProvider">Объект `ErrorProvider` для отображения ошибок валидации на контроле (опционально).</param>
        public static void AddBinding<TControl, TSource>(this TControl target,
            Expression<Func<TControl, object>> targetProperty,
            TSource source,
            Expression<Func<TSource, object>> sourceProperty,
            ErrorProvider errorProvider = null)
            where TControl : Control
            where TSource : class
        {
            var targetName = GetMemberName(targetProperty);
            var sourceName = GetMemberName(sourceProperty);

            target.DataBindings.Add(new Binding(targetName, source, sourceName,
            false,
                DataSourceUpdateMode.OnPropertyChanged));

            if (errorProvider != null)
            {
                var sourcePropertyInfo = source.GetType().GetProperty(sourceName);
                var validators = sourcePropertyInfo?.GetCustomAttributes<ValidationAttribute>();
                if (validators?.Any() == true)
                {
                    target.Validating += (sender, args) =>
                    {
                        var context = new ValidationContext(source);
                        var results = new List<ValidationResult>();
                        errorProvider.SetError(target, string.Empty);
                        if (!Validator.TryValidateObject(source, context, results, validateAllProperties: true))
                        {
                            foreach (var error in results.Where(x => x.MemberNames.Contains(sourceName)))
                            {
                                errorProvider.SetError(target, error.ErrorMessage);
                            }
                        }
                    };
                }
            }
        }

        private static string GetMemberName<TItem, TMember>(Expression<Func<TItem, TMember>> targetMember)
        {
            if (targetMember.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            if (targetMember.Body is UnaryExpression unaryExpression)
            {
                var operand = unaryExpression.Operand as MemberExpression;
                return operand.Member.Name;
            }

            throw new ArgumentException();
        }
    }
}
