namespace ProductManagementCatalogue.Domain.Shared.Results;

public class Result
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;

	public IEnumerable<string> Errors { get; }

	protected Result(bool isSuccess, IEnumerable<string> errors)
	{
		IsSuccess = isSuccess;
		Errors = errors ?? [];
	}

	public static Result Success() => new(isSuccess: true, errors: []);

	public static Result Failure(string error) => new(isSuccess: false, [error]);

	public static Result Failure(IEnumerable<string> errors) => new(isSuccess: false, errors);
}

public class Result<T> : Result
{
	private readonly T _value;

	public T Value
	{
		get
		{
			if (IsFailure)
				throw new InvalidOperationException("Cannot access the value of a failed result. Check IsSuccess first.");

			return _value;
		}
	}

	protected Result(T value, bool isSuccess, IEnumerable<string> errors)
		: base(isSuccess, errors)
	{
		_value = value;
	}

	public static Result<T> Success(T value) => new(value, isSuccess: true, errors: []);

	public new static Result<T> Failure(string error) => new(default, isSuccess: false, errors: [error]);

	public new static Result<T> Failure(IEnumerable<string> errors) => new(default, isSuccess: false, errors);
}