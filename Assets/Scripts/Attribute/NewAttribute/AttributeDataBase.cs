public abstract class AttributeDataBase {
    public AttributeType type = new AttributeType();
    public abstract string GetValue();
    public abstract void GenerateParameters();
}
