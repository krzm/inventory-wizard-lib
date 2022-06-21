using CLIReader;
using CLIWizardHelper;
using Inventory.Data;
using Serilog;

namespace Inventory.Wizard.Lib;

public class ImageUpdateWizard 
	: UpdateWizard<IInventoryUnitOfWork, Image>
{
    public ImageUpdateWizard(
		IInventoryUnitOfWork unitOfWork
		, IReader<string> requiredTextReader
		, ILogger log) 
			: base(unitOfWork, requiredTextReader, log)
    {
    }

    protected override Image GetById(int id)
    {
        return UnitOfWork.Image.GetByID(id);
	}

    protected override void UpdateEntity(int nr, Image model)
    {
        switch (nr)
        {
			case 1:
				var input = RequiredTextReader.Read(
					new ReadConfig(6, nameof(Image.ItemId)));
				ArgumentNullException.ThrowIfNull(input);
				model.ItemId = int.Parse(input);
				break;
			case 2:
				model.Path = RequiredTextReader.Read(
					new ReadConfig(250, nameof(Image.Path)));
				break;
		}
	}
}