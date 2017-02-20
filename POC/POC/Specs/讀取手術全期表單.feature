Feature: 讀取手術全期表單


Scenario: 啟動空白表單
When 按下新增表單
Then 得到空白表單

Scenario: 啟動測試表單01
When 表單名稱 測試表單01，按下新增表單
Then 得到空白表單，名稱 測試表單01