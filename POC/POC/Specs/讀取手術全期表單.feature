Feature: 讀取手術全期表單


Scenario: 啟動空白表單
When 按下新增表單
Then 得到空白表單

Scenario: 依照定義取得空白表單
Given 表單定義
| Name | 
| 表單01 |
Given 欄位定義
| Name | FormName | ColumnType   |
| 欄位01 | 表單01     | StringColumn |
| 欄位02 | 表單01     | CheckColumn  |

When 表單名稱："表單01"，按下新增表單

Then 得到表單內容
| Name |
| 表單01 |
Then 得到表單欄位
| Name |  ColumnType   |
| 欄位01 | StringColumn |
| 欄位02 | CheckColumn  |
Then 欄位："欄位01"，型別："StringColumn"
