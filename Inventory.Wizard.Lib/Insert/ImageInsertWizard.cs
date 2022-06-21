using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ImageInsertWizard 
	: InsertWizard<IInventoryUnitOfWork, Image>
{
	public ImageInsertWizard(
	   IInventoryUnitOfWork unitOfWork
	   , IReader<string> requiredTextReader
	   , ILogger log)
		   : base(unitOfWork, requiredTextReader, log)
	{
	}

	protected override Image GetEntity()
	{
		var input = RequiredTextReader.Read(new ReadConfig(6, nameof(Image.ItemId)));
		ArgumentNullException.ThrowIfNull(input);
		return new Image()
		{
			ItemId = int.Parse(input)
			,
			Path = RequiredTextReader.Read(new ReadConfig(250, nameof(Image.Path)))
		};
	}

	protected override void InsertEntity(Image entity) =>
		UnitOfWork.Image.Insert(entity);
}