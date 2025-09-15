public class Computer {
    // Required parameters
    private final String cpu;
    private final String memory;
    
    // Optional parameters
    private final String storage;
    private final String graphicsCard;
    private final boolean isGaming;
    private final boolean hasWiFi;
    private final String operatingSystem;
    
    // Private constructor - only builder can create instances
    private Computer(ComputerBuilder builder) {
        this.cpu = builder.cpu;
        this.memory = builder.memory;
        this.storage = builder.storage;
        this.graphicsCard = builder.graphicsCard;
        this.isGaming = builder.isGaming;
        this.hasWiFi = builder.hasWiFi;
        this.operatingSystem = builder.operatingSystem;
    }
    
    // Getters
    public String getCpu() { return cpu; }
    public String getMemory() { return memory; }
    public String getStorage() { return storage; }
    public String getGraphicsCard() { return graphicsCard; }
    public boolean isGaming() { return isGaming; }
    public boolean hasWiFi() { return hasWiFi; }
    public String getOperatingSystem() { return operatingSystem; }
    
    @Override
    public String toString() {
        return "Computer{" +
                "cpu='" + cpu + '\'' +
                ", memory='" + memory + '\'' +
                ", storage='" + storage + '\'' +
                ", graphicsCard='" + graphicsCard + '\'' +
                ", isGaming=" + isGaming +
                ", hasWiFi=" + hasWiFi +
                ", operatingSystem='" + operatingSystem + '\'' +
                '}';
    }
    
    // Static nested Builder class
    public static class ComputerBuilder {
        // Required parameters
        private final String cpu;
        private final String memory;
        
        // Optional parameters with default values
        private String storage = "256GB SSD";
        private String graphicsCard = "Integrated";
        private boolean isGaming = false;
        private boolean hasWiFi = true;
        private String operatingSystem = "Windows 11";
        
        // Builder constructor with required parameters
        public ComputerBuilder(String cpu, String memory) {
            this.cpu = cpu;
            this.memory = memory;
        }
        
        // Methods to set optional parameters
        public ComputerBuilder storage(String storage) {
            this.storage = storage;
            return this;
        }
        
        public ComputerBuilder graphicsCard(String graphicsCard) {
            this.graphicsCard = graphicsCard;
            return this;
        }
        
        public ComputerBuilder isGaming(boolean isGaming) {
            this.isGaming = isGaming;
            return this;
        }
        
        public ComputerBuilder hasWiFi(boolean hasWiFi) {
            this.hasWiFi = hasWiFi;
            return this;
        }
        
        public ComputerBuilder operatingSystem(String operatingSystem) {
            this.operatingSystem = operatingSystem;
            return this;
        }
        
        // Build method creates the Computer instance
        public Computer build() {
            // Validation logic can be added here
            if (isGaming && graphicsCard.equals("Integrated")) {
                throw new IllegalStateException("Gaming computer needs dedicated graphics card");
            }
            return new Computer(this);
        }
    }
}

// Usage example
class BuilderExample {
    public static void main(String[] args) {
        // Cannot create Computer directly:
        // Computer computer = new Computer(...); // ERROR!
        
        // Must use builder pattern
        Computer basicComputer = new Computer.ComputerBuilder("Intel i5", "8GB")
                .build();
        
        Computer gamingComputer = new Computer.ComputerBuilder("AMD Ryzen 9", "32GB")
                .storage("1TB NVMe SSD")
                .graphicsCard("RTX 4080")
                .isGaming(true)
                .operatingSystem("Windows 11 Pro")
                .build();
        
        Computer workStation = new Computer.ComputerBuilder("Intel Xeon", "64GB")
                .storage("2TB SSD")
                .graphicsCard("Quadro RTX 6000")
                .hasWiFi(false)
                .operatingSystem("Ubuntu 22.04")
                .build();
        
        System.out.println("Basic Computer:");
        System.out.println(basicComputer);
        
        System.out.println("\nGaming Computer:");
        System.out.println(gamingComputer);
        
        System.out.println("\nWork Station:");
        System.out.println(workStation);
        
        // Builder validates configuration
        try {
            Computer invalidGamingPC = new Computer.ComputerBuilder("Intel i3", "4GB")
                    .isGaming(true)  // Gaming with integrated graphics
                    .build(); // Will throw exception
        } catch (IllegalStateException e) {
            System.out.println("\nValidation Error: " + e.getMessage());
        }
    }
}