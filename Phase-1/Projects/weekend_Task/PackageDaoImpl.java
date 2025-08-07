import java.util.ArrayList;
import java.util.List;

public class PackageDaoImpl implements PackageDao {
    private final List<Package> packages = new ArrayList<>();

    @Override
    public void addPackage(Package pkg) {
        packages.add(pkg);
    }

    @Override
    public List<Package> getAllPackages() {
        return packages;
    }

    @Override
    public Package findPackageById(String id) {
        for (Package pkg : packages) {
            if (pkg.getPackageId().equals(id)) return pkg;
        }
        return null;
    }

    @Override
    public void calculatePackageCost(String id) throws InvalidPackageIdException {
        Package pkg = findPackageById(id);
        if (pkg == null) throw new InvalidPackageIdException("Invalid Package Id");

        double baseCost = pkg.getBasicFare() * pkg.getNoOfDays();
        double discount = 0;
        int days = pkg.getNoOfDays();

        if (days > 5 && days <= 8) discount = baseCost * 0.03;
        else if (days > 8 && days <= 10) discount = baseCost * 0.05;
        else if (days > 10) discount = baseCost * 0.07;

        double gst = (baseCost - discount) * 0.12;
        double totalCost = (baseCost - discount) + gst;
        pkg.setPackageCost(totalCost);
    }
}
