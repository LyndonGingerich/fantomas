module Fantomas.TriviaTypes

open FSharp.Compiler.SourceCodeServices
open FSharp.Compiler.Range

type Token =
    { TokenInfo:FSharpTokenInfo
      LineNumber: int
      Content: string }


type Comment =
    | LineCommentAfterSourceCode of comment:string
    | LineCommentOnSingleLine of comment:string
    | BlockComment of string
    
(* LineComment Examples

let a = 7 // b

=> LineCommentAfterSourceCode("// b", true)

// meh
let a = 7

=> LineCommentOnSingleLine("// meh", false)
*)
    
type TriviaContent =
    | Keyword of string
    | Comment of Comment
    | Newline
    | Directive of string
    
type Trivia =
    { Item: TriviaContent
      Range: range }
with
    static member Create item range : Trivia =
        { Item = item; Range = range }
        
type TriviaIndex = TriviaIndex of int * int

type TriviaNodeType =
    | MainNode of ``type``:string
    | Token of Token
    
type TriviaNode =
  { Type: TriviaNodeType
    ContentBefore: TriviaContent list
    ContentAfter: TriviaContent list
    Range: range }