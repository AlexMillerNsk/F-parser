module Types


type Message =
    { Id: int
      Text: string }

type Root =
    { Id: int
      Name: string
      Type: string
      Messages: Message }

type ErrType = Debug | Error | Info

type ErrMessage =
    { Id: int
      Time: string
      Type: ErrType
      Text: string }
