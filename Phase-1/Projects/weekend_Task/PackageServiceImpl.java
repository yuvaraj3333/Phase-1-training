import java.util.List;

public class PackageServiceImpl implements PackageService {
    private final PackageDao dao = new PackageDaoImpl();

    @Override
    public void addPackage(Package pkg) throws InvalidPackageIdException {
        if (pkg.getPackageId().length() != 7) throw new InvalidPackageIdException("Invalid Package Id");
        dao.addPackage(pkg);
    }

    @Override
    public List<Package> fetchAllPackages() {
        return dao.getAllPackages();
    }

    @Override
    public Package findPackageById(String id) {
        return dao.findPackageById(id);
    }

    @Override
    public void calculatePackageCost(String id) throws InvalidPackageIdException {
        dao.calculatePackageCost(id);
    }
}
