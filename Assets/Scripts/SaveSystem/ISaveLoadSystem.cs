public interface ISaveLoadSystem {
    void Save(string key, object data);
    T Load<T>(string key);
}
