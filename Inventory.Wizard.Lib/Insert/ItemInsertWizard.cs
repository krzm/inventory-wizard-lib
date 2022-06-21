using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ItemInsertWizard 
	: InsertWizard<IInventoryUnitOfWork, Item>
{
	public ItemInsertWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, ILogger log) 
			: base(unitOfWork, requiredTextReader, log)
	{
	}

	protected override Item GetEntity()
	{
		var input = RequiredTextReader.Read(
			new ReadConfig(6, nameof(Item.CategoryId)));
		ArgumentNullException.ThrowIfNull(input);
		return new Item
		{
			CategoryId = int.Parse(input)
			,
			Name = RequiredTextReader.Read(
				new ReadConfig(25, nameof(Item.Name)))
		};
	}

	protected override void InsertEntity(Item entity) =>
		UnitOfWork.Item.Insert(entity);
}