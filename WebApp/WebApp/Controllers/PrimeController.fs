namespace WebApp.Controllers

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open WebApp

[<ApiController>]
[<Route("[controller]")>]
type PrimeController(logger : ILogger<PrimeController>) = 
    inherit ControllerBase()
    
    let rec primes = 
        seq {
            yield 2
            let mutable x = 3
            while true do
                if isprime x then 
                    yield x
                x <- x + 2
        }
    and isprime x =
        use e = primes.GetEnumerator()
        let rec loop() =
            if e.MoveNext() then
                let p = e.Current
                if p * p > x then true
                elif x % p = 0 then false
                else loop()
            else true            
        loop()
               

    [<HttpGet()>]
    
    member __.Get(id) : Prime =
        { Index = id
          Value = primes |> Seq.item (id)
        }
        
        
       