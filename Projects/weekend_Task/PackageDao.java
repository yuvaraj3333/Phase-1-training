import java.util.List;

public interface PackageDao {
    void addPackage(Package pkg);
    List<Package> getAllPackages();
    Package findPackageById(String id);
    void calculatePackageCost(String id) throws InvalidPackageIdException;
}
