import java.util.List;

public interface PackageService {
    void addPackage(Package pkg) throws InvalidPackageIdException;
    List<Package> fetchAllPackages();
    Package findPackageById(String id);
    void calculatePackageCost(String id) throws InvalidPackageIdException;
}
