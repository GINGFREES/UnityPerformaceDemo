# Performance Impact

Device info:

Mac mini 2020
* M1 8 核心
* Memory 16G
* 顯示器: 1920x1080 240Hz (ZOWIE XL LCD)

## CPU Usage

|Case|percentage|effct show percentage on average|
|:--|:--|:--|
|None of executing|60%~100%||
|Fps 30|75%~85%|78%~80%|
|Fps 60|80%~95%|84%~87%|
|Fps 90|85%~100%|88%~91%|
|Fps 120|90%~105%|91%~93%|

推估耗能 純以 cpu 計算-
- FPS 60 約為 FPS 30 耗能的 1.08 倍
- FPS 90 約為 FPS 60 耗能的 1.04 倍
- FPS 120 約為 FPS 90 耗能的 1.03 倍


## 圖片記憶體使用

序列圖記憶體推算 -  以 200x200 (空白 margin 約 38) 大小推算, Max 限制 2048 x 2048
- Normal Quality
- 30 張序列圖 SpriteAtlas 使用
  - 1024X1024 (1MB)
- 60 張序列圖 SpriteAtlas 使用
  - 1024x2048 (2MB) (未 force square)
- 90 張序列圖 SpriteAtlas 使用
  - 2048x2048 (4MB)
- 120 張序列圖 SpriteAtlas 使用
  - 2048x2048 (4MB)
- 240 張序列圖 SpriteAtlas 使用
  - 有 2 張圖
  - 2 張 2048x2048
  - 總共 8MB
- 360 張序列圖 SpriteAtlas 使用
  - 有 3 張圖
  - 2 張 2048x2048(8MB)
  - 1 張 1024x1024(1MB)
  - 總共 9MB

評估: 
* 受 Unity 本身壓縮方式限制, 若使用第三方工具可能可以再精簡些
* 若強制為 2^n 圖片, 部分數量圖片會佔用更大空間(ex, 60 張序列圖會用到 2048x2048)


## 耗電量實測 iPad pro

* -1(系統預設)
  * 1小時消耗 8%

* 30fps
  * 1 小時消耗 2%

