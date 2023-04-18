namespace lab5;
public class Data {
    public int producerNumber;

    public Data(Producer producer) {
        producerNumber = producer.number;
    }

    public override string ToString()
    {
        return "Dane producenta numer " + producerNumber;
    }
}