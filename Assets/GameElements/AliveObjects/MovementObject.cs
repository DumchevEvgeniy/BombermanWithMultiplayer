using System;

public abstract class MovementObject : DynamicGameObject {
    public Single RotationSpeed { get; set; }
    public Single MovementSpeed { get; set; }
    public Boolean Wallpass { get; set; }
}