# ToDo.UWP
This is a To do list app that uses UWP. App's basic function includes adding, checking off and searching for a task.

## User Guide:
### Adding a task:
1. Type the new task on the text box at the bottom
2. Press `Enter key` on your keyboard or hit the `Add` button on the right of the text box.
![adding](https://user-images.githubusercontent.com/23254953/161419612-bb90e77e-03ac-4272-bd4b-7e0a7877084c.gif)

### Checking on/off 
1. Select a task to toggle checking status
![Checking off](https://user-images.githubusercontent.com/23254953/161419646-7eb04c38-788b-48f1-add8-96b5494d496d.gif)

### Search
1. Type keywords of task in the search text box at the top
![searching](https://user-images.githubusercontent.com/23254953/161419677-274de183-4fe2-489f-b3f8-019246e923e7.gif)

## Developer Guide:
### Updatating search algorithm:
Current search uses Levenshtein distance [^1]. If you want to implement your own string metric then do the following:
1. Add a class and implement `IStringDistance`
2. Add the implementation inside `Compute`
3. replace `_stringMetric` in `CollectionViewBehavior`

```
public class CollectionViewBehavior : Behavior<ListView>
{
...
  private readonly IStringDistance _stringMetric = new LevenshteinDistance();
...
}
```

[^1]: [Levenshtein distance](https://en.wikipedia.org/wiki/Levenshtein_distance#:~:text=Informally%2C%20the%20Levenshtein%20distance%20between,considered%20this%20distance%20in%201965.)
