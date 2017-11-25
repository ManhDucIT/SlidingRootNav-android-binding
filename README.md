# SlidingRootNav-android-binding
This is an Android Binding library for SlidingRootNav which is a DrawerLayout-like ViewGroup, where a "drawer" is hidden under the content view, which can be shifted to make the drawer visible. It doesn't provide you with a drawer builder.

![sliding_root_nav](https://user-images.githubusercontent.com/24780565/33228042-87a51514-d1e4-11e7-83ef-b1846d18970f.gif)

## Installation 
Install-Package SlidingRootNav

## Sample
Please see the [sample app](sample/SlidingRootNavTest/Resources/layout/activity_main.xml) for a library usage example.

## Usage:
 1. Create your content_view.xml ([example](sample/SlidingRootNavTest/Resources/layout/activity_main.xml)) or construct a `View` programatically.
 2. Set the content view (for example, using `SetContentView` in your activity).
 3. Create your menu.xml ([example](sample/SlidingRootNavTest/Resources/layout/menu_left_drawer.xml)) or construct a `View` programatically.
 4. Now you need to inject the menu in your `OnCreate`. You can specify transformations of a content view or use the default ones. 
```c#
new SlidingRootNavBuilder(this)
  .WithMenuLayout(Resource.Layout.menu_left_drawer)
  .Inject();
```

### API
#### Transformations
You can specify root transformations using `SlidingRootNavBuilder`.
```C#
new SlidingRootNavBuilder(this)
  .WithDragDistance(140) //Horizontal translation of a view. Default == 180dp
  .WithRootViewScale(0.7f) //Content view's scale will be interpolated between 1f and 0.7f. Default == 0.65f;
  .WithRootViewElevation(10) //Content view's elevation will be interpolated between 0 and 10dp. Default == 8.
  .WithRootViewYTranslation(4) //Content view's translationY will be interpolated between 0 and 4. Default == 0
  .AddRootTransformation(customTransformation)
  .Inject();
```

#### Menu behavior
```C#
new SlidingRootNavBuilder(this)
  .WithMenuOpened(true) //Initial menu opened/closed state. Default == false
  .WithMenuLocked(false) //If true, a user can't open or close the menu. Default == false.
  .WithGravity(SlideGravity.Left) //If Left you can swipe a menu from left to right, if Right - the direction is opposite. 
  .WithSavedState(savedInstanceState) //If you call the method, layout will restore its opened/closed state
  .WithContentClickableWhenMenuOpened(isClickable) //Pretty self-descriptive. Builder Default == true
```
#### Controling the layout
A call to `Inject()` returns you an interface for controlling the layout.
```C#
public interface ISlidingRootNav {
    boolean IsMenuClosed{ get; };
    boolean IsMenuOpened{ get; };
    boolean IsMenuLocked{ get; set; };
    void CloseMenu();
    void CloseMenu(boolean animated);
    void OpenMenu();
    void OpenMenu(boolean animated);
    void SetMenuLocked(boolean locked);
    SlidingRootNavLayout Layout{ get; }; //If for some reason you need to work directly with layout - you can
}
```

#### Callbacks
* Drag progress:
```C#
builder.AddDragListener(listener);

public interface IDragListener {
  void OnDrag(float progress); //Float between 0 and 1, where 1 is a fully visible menu
}

```
* Drag state changes:
```C#
builder.AddDragStateListener(listener);

public interface IDragStateListener {
  void OnDragStart();
  void OnDragEnd(boolean isMenuOpened);
}
```

* Compatibility with `DrawerLayout.DrawerListener`:
```C#
DrawerListenerAdapter adapter = new DrawerListenerAdapter(yourDrawerListener, viewToPassAsDrawer);
builder.AddDragListener(listenerAdapter).AddDragStateListener(listenerAdapter);
```

## Special thanks
Thanks to [Yaroslav](https://github.com/yarolegovich) for a native wonderful initial library [SlidingRootNav
](https://github.com/yarolegovich/SlidingRootNav).

## License
```
MIT License

Copyright (c) 2017 Dang Manh Duc

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
