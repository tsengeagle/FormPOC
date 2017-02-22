Feature: 產生空白表單


Scenario: 依據定義產生表單
Given 表單定義
| Name   |
| Form01 |
Given 欄位定義
| Name  | FormName | DefaultValue |
| Col01 | Form01   | test         |

When 取得表單："Form01"

Then 得到表單："Form01"，欄位：
| Name  | Value |
| Col01 | test  |