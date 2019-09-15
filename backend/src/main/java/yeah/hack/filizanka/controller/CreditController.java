package yeah.hack.filizanka.controller;

import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import yeah.hack.filizanka.controller.dto.SkinDto;
import yeah.hack.filizanka.service.CreditService;

@RestController
@RequestMapping("/store")
@RequiredArgsConstructor
public class CreditController {

    private final CreditService creditService;

    @PostMapping("/{user-id}/skin/{skin-id}")
    public SkinDto exchangeCreditsForSkin(@PathVariable(value = "user-id") Long userId,
                                          @PathVariable(value = "skin-id") Long skinId) {
        return SkinDto.from(creditService.exchangeCreditsForSkin(userId,skinId));
    }

}
