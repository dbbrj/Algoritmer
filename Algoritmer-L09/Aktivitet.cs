/*
 * Opgave i grafer/aktivitetsplanlægning (lektion 9 og 10):
 *
 *   Lav en klasse ved navn Aktivitet, som indeholder attributterne event (int),
 *   task (string) og duration (int) med de sædvanlige metoder (constructors,
 *   get-metoder).
 *
 * "event" er et reserveret ord i C#, så attributten skrives @event.
 * Navnet er stadig event, som specifikationen kræver.
 */
public class Aktivitet
{
    private int @event;
    private string task;
    private int duration;

    public Aktivitet(int @event, string task, int duration)
    {
        this.@event = @event;
        this.task = task;
        this.duration = duration;
    }

    public int GetEvent()
    {
        return @event;
    }

    public string GetTask()
    {
        return task;
    }

    public int GetDuration()
    {
        return duration;
    }

    public override string ToString()
    {
        return $"{@event};{task};{duration}";
    }
}
