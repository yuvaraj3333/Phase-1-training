public class Package {
    private final String packageId;
    private final String sourcePlace;
    private final String destinationPlace;
    private final int noOfDays;
    private final double basicFare;
    private double packageCost;

    public Package(String packageId, String sourcePlace, String destinationPlace, int noOfDays, double basicFare) {
        this.packageId = packageId;
        this.sourcePlace = sourcePlace;
        this.destinationPlace = destinationPlace;
        this.noOfDays = noOfDays;
        this.basicFare = basicFare;
        this.packageCost = 0;
    }

    public String getPackageId() { return packageId; }
    public String getSourcePlace() { return sourcePlace; }
    public String getDestinationPlace() { return destinationPlace; }
    public int getNoOfDays() { return noOfDays; }
    public double getBasicFare() { return basicFare; }
    public double getPackageCost() { return packageCost; }
    public void setPackageCost(double packageCost) { this.packageCost = packageCost; }

    @Override
    public String toString() {
        return String.format("Package ID: %s, Source: %s, Destination: %s, Days: %d, Basic Fare: %.2f, Package Cost: %.2f",
                packageId, sourcePlace, destinationPlace, noOfDays, basicFare, packageCost);
    }
}
