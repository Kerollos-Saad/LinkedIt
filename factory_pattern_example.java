// Shape interface
interface Shape {
    void draw();
    double calculateArea();
}

// Concrete Shape classes
class Circle implements Shape {
    private double radius;
    
    // Private constructor - only factory can create instances
    private Circle(double radius) {
        this.radius = radius;
    }
    
    @Override
    public void draw() {
        System.out.println("Drawing a circle with radius: " + radius);
    }
    
    @Override
    public double calculateArea() {
        return Math.PI * radius * radius;
    }
    
    // Factory method
    public static Circle create(double radius) {
        if (radius <= 0) {
            throw new IllegalArgumentException("Radius must be positive");
        }
        return new Circle(radius);
    }
}

class Rectangle implements Shape {
    private double width, height;
    
    // Private constructor
    private Rectangle(double width, double height) {
        this.width = width;
        this.height = height;
    }
    
    @Override
    public void draw() {
        System.out.println("Drawing a rectangle: " + width + " x " + height);
    }
    
    @Override
    public double calculateArea() {
        return width * height;
    }
    
    // Factory method with validation
    public static Rectangle create(double width, double height) {
        if (width <= 0 || height <= 0) {
            throw new IllegalArgumentException("Dimensions must be positive");
        }
        return new Rectangle(width, height);
    }
}

// Shape Factory
class ShapeFactory {
    // Private constructor - utility class
    private ShapeFactory() {}
    
    public static Shape createCircle(double radius) {
        return Circle.create(radius);
    }
    
    public static Shape createRectangle(double width, double height) {
        return Rectangle.create(width, height);
    }
    
    public static Shape createShape(String type, double... dimensions) {
        switch (type.toLowerCase()) {
            case "circle":
                if (dimensions.length != 1) {
                    throw new IllegalArgumentException("Circle requires 1 dimension (radius)");
                }
                return createCircle(dimensions[0]);
            case "rectangle":
                if (dimensions.length != 2) {
                    throw new IllegalArgumentException("Rectangle requires 2 dimensions (width, height)");
                }
                return createRectangle(dimensions[0], dimensions[1]);
            default:
                throw new IllegalArgumentException("Unknown shape type: " + type);
        }
    }
}

// Usage example
class FactoryExample {
    public static void main(String[] args) {
        // Cannot create shapes directly:
        // Circle circle = new Circle(5.0); // ERROR!
        
        // Must use factory methods
        Shape circle = ShapeFactory.createCircle(5.0);
        Shape rectangle = ShapeFactory.createRectangle(4.0, 6.0);
        
        // Or use the generic factory method
        Shape circle2 = ShapeFactory.createShape("circle", 3.0);
        Shape rectangle2 = ShapeFactory.createShape("rectangle", 2.0, 8.0);
        
        // Use the shapes
        circle.draw();
        System.out.println("Circle area: " + circle.calculateArea());
        
        rectangle.draw();
        System.out.println("Rectangle area: " + rectangle.calculateArea());
        
        // Factory ensures validation
        try {
            Shape invalidCircle = ShapeFactory.createCircle(-1.0); // Will throw exception
        } catch (IllegalArgumentException e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
}