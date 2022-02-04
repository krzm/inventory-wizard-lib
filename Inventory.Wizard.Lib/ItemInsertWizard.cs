using CLIHelper;
using CLIReader;
using CLIWizardHelper;
using Inventory.Data;

namespace Inventory.Wizard.Lib;

public class ItemInsertWizard 
	: InsertWizard<IInventoryUnitOfWork, Item>
{
	public ItemInsertWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, IOutput output) 
			: base(unitOfWork, requiredTextReader, output)
	{
	}

	protected override Item GetEntity()
	{
		return new Item
		{
			ItemCategoryId = int.Parse(RequiredTextReader.Read(
				new ReadConfig(6, nameof(Item.ItemCategoryId))))
			,
			Name = RequiredTextReader.Read(
				new ReadConfig(25, nameof(Item.Name)))
		};
	}

	protected override void InsertEntity(Item entity) =>
		UnitOfWork.Item.Insert(entity);
}