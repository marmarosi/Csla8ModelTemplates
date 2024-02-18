using Csla.Rules;
using Csla.Rules.CommonRules;

namespace Csla8RestApi.Models.Validations
{
    /// <summary>
    /// Business rule for a non-zero value.
    /// </summary>
    public class NotZero<T> : CommonBusinessRule
      where T : IComparable
    {
#pragma warning disable S1144, S3459 // Unused private types or members should be removed
        private T? Zero { get; set; }
#pragma warning restore S1144, S3459 // Unused private types or members should be removed

        /// <summary>
        /// Gets or sets the format string used
        /// to format the Min value.
        /// </summary>
        public string? Format { get; set; }

        /// <summary>
        /// Creates an instance of the rule.
        /// </summary>
        /// <param name="primaryProperty">Property to which the rule applies.</param>
        public NotZero(Csla.Core.IPropertyInfo primaryProperty)
          : base(primaryProperty)
        {
            InputProperties.Add(primaryProperty);
        }

        /// <summary>
        /// Creates an instance of the rule.
        /// </summary>
        /// <param name="primaryProperty">Property to which the rule applies.</param>
        /// <param name="message">The message.</param>
        public NotZero(Csla.Core.IPropertyInfo primaryProperty, string message)
          : this(primaryProperty)
        {
            MessageText = message;
        }

        /// <summary>
        /// Creates an instance of the rule.
        /// </summary>
        /// <param name="primaryProperty">Property to which the rule applies.</param>
        /// <param name="messageDelegate">The localizable message.</param>
        public NotZero(Csla.Core.IPropertyInfo primaryProperty, Func<string> messageDelegate)
          : this(primaryProperty)
        {
            MessageDelegate = messageDelegate;
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value></value>
        protected override string GetMessage()
        {
            return HasMessageDelegate ? base.MessageText : "{0} value must not be {1}.";
        }

        /// <summary>
        /// Rule implementation.
        /// </summary>
        /// <param name="context">Rule context.</param>
        protected override void Execute(
#pragma warning disable CSLA0017
            IRuleContext context
#pragma warning restore CSLA0017
            )
        {
#pragma warning disable S3358
            var value = context.InputPropertyValues[PrimaryProperty] is not null
                            ? (T)context.InputPropertyValues[PrimaryProperty]
                            : PrimaryProperty.DefaultValue is not null
                                ? (T)PrimaryProperty.DefaultValue
                                : default;
#pragma warning restore S3358

            var result = value!.CompareTo(Zero);
            if (result == 0)
            {
                string outValue;
                if (string.IsNullOrEmpty(Format))
                    outValue = "0";
                else
                    outValue = string.Format(string.Format("{{0:{0}}}", Format), Zero);
                var message = string.Format(GetMessage(), PrimaryProperty.FriendlyName, outValue);
                context.Results.Add(new RuleResult(RuleName, PrimaryProperty, message) { Severity = Severity });
            }
        }
    }
}
